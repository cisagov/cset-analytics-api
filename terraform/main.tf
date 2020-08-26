# ===================================
# Document DB
# ===================================
resource "random_string" "docdb_username" {
  length  = 8
  number  = false
  special = false
  upper   = false
}

resource "aws_ssm_parameter" "docdb_username" {
  name        = "/${var.env}/${var.app}/docdb/username/master"
  description = "The username for document db"
  type        = "SecureString"
  value       = random_string.docdb_username.result
}

resource "random_password" "docdb_password" {
  length           = 32
  special          = true
  override_special = "!_#&"
}

resource "aws_ssm_parameter" "docdb_password" {
  name        = "/${var.env}/${var.app}/docdb/password/master"
  description = "The password for document db"
  type        = "SecureString"
  value       = random_password.docdb_password.result
}

module "documentdb" {
  source                  = "github.com/cloudposse/terraform-aws-documentdb-cluster"
  stage                   = "${var.env}"
  name                    = "${var.env}-${var.app}-docdb"
  cluster_size            = 1
  master_username         = random_string.docdb_username.result
  master_password         = random_password.docdb_password.result
  instance_class          = var.docdb_instance_class
  vpc_id                  = var.vpc_id
  subnet_ids              = var.public_subnet_ids
  allowed_cidr_blocks     = ["0.0.0.0/0"]
  allowed_security_groups = [aws_security_group.api.id]
  skip_final_snapshot     = true
}

# ===================================
# Container Definition
# ===================================
locals {
  api_port    = 80
  api_lb_port = 8443

  environment = {
    "DB_HOST" : module.documentdb.endpoint,
    "DB_PORT" : 27017,
    "DB_PARAMS" : "?authSource=admin&ssl=true&readpreference=primary&tlsInsecure=true",
    "MONGO_TYPE" : "DOCUMENTDB"
  }

  secrets = {
    "DB_PW" : aws_ssm_parameter.docdb_password.arn,
    "DB_USER" : aws_ssm_parameter.docdb_username.arn,
  }
}

module "api_container" {
  source    = "github.com/cisagov/fargate-container-def-tf-module"
  namespace = var.app
  stage     = var.env
  name      = "api"

  container_name  = "${var.app}-api"
  container_image = "${var.image_repo}:${var.image_tag}"
  container_port  = local.api_port
  region          = var.region
  log_retention   = 7
  environment     = local.environment
  secrets         = local.secrets
}

# ===================================
# Fargate Service
# ===================================
module "api_fargate" {
  source    = "github.com/cisagov/fargate-service-tf-module"
  namespace = var.app
  stage     = var.env
  name      = "api"

  iam_server_cert_arn   = aws_acm_certificate.cert.arn
  container_port        = local.api_port
  container_definition  = module.api_container.json
  container_name        = "${var.app}-api"
  cpu                   = 2048
  memory                = 4096
  vpc_id                = var.vpc_id
  health_check_interval = 60
  health_check_path     = "/api/ping/GetPing"
  health_check_codes    = "200"
  load_balancer_arn     = module.alb.alb_arn
  load_balancer_port    = local.api_lb_port
  desired_count         = 1
  subnet_ids            = var.private_subnet_ids
  security_group_ids    = [aws_security_group.api.id]
}

# ===================================
# Security Groups
# ===================================
resource "aws_security_group" "api" {
  name        = "${var.app}-${var.env}-api-alb"
  description = "Allow traffic for api from alb"
  vpc_id      = var.vpc_id

  ingress {
    description     = "Allow container port from ALB"
    from_port       = local.api_port
    to_port         = local.api_port
    protocol        = "tcp"
    security_groups = [aws_security_group.alb.id]
    self            = true
  }

  egress {
    description = "Allow outbound traffic"
    from_port   = 0
    to_port     = 0
    protocol    = -1
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    "Name" = "${var.app}-${var.env}-api-alb"
  }
}
