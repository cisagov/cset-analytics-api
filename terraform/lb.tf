# ===================================
# Load Balancer
# ===================================
resource "aws_security_group" "alb" {
  name        = "${var.app}-${var.env}-alb"
  description = "Allowed ports into alb"
  vpc_id      = var.vpc_id

  ingress {
    from_port   = 0
    to_port     = 0
    protocol    = -1
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    "Name" = "${var.app}-${var.env}-alb"
  }
}

module "alb" {
  source             = "github.com/cloudposse/terraform-aws-alb"
  namespace          = var.app
  stage              = var.env
  name               = "public-alb"
  http_enabled       = false
  internal           = false
  vpc_id             = var.vpc_id
  security_group_ids = [aws_security_group.alb.id]
  subnet_ids         = var.public_subnet_ids
  target_group_name  = "${var.app}-${var.env}-tg"
}

# ===================================
# Listener
# ===================================
resource "aws_lb_listener" "https" {
    load_balancer_arn = module.alb.alb_arn
    port              = 443
    protocol          = "HTTPS"
    certificate_arn   = aws_acm_certificate.cert.arn

    default_action {
        type = "fixed-response"

        fixed_response {
            content_type = "text/plain"
            message_body = "${var.app}-${var.env} fixed response"
            status_code  = 200
        }
    }
}

# ===================================
# Listener Rule
# ===================================
resource "aws_lb_listener_rule" "api" {
  listener_arn = aws_lb_listener.https.arn
  priority     = 100

  action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.api.arn
  }

  condition {
    path_pattern {
      values = ["/api/*"]
    }
  }
}

#=========================
# TARGET GROUP
#=========================
resource "aws_lb_target_group" "api" {
  name        = "${var.app}-${var.env}-api"
  port        = local.api_port
  protocol    = local.api_protocol
  target_type = "ip"
  vpc_id      = var.vpc_id

  health_check {
    healthy_threshold   = 3
    interval            = 60
    matcher             = "200"
    path                = "/api/ping/GetPing"
    port                = local.api_port
    protocol            = local.api_protocol
    unhealthy_threshold = 3
  }
}
