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
