locals {
  api_port     = 80
  api_protocol = "HTTP"
  api_lb_port  = 8443

  container_name = "${var.app}-api"

  environment = {
    "DB_HOST" : module.documentdb.endpoint,
    "DB_PORT" : 27017,
    "DB_PARAMS" : "?authSource=admin&ssl=true&readpreference=primary&tlsInsecure=true",
    "MONGO_TYPE" : "DOCUMENTDB",
    "COGNITO_REGION": var.region,
    "COGNITO_POOL_ID": aws_cognito_user_pool.pool.id,
    "COGNITO_CLIENT_ID": aws_cognito_user_pool_client.client.id,
    "CORS_CSET_ORIGINS": "https://${aws_route53_record.domain.name},https://${module.alb.alb_dns_name},https://*.${var.route53_zone_name}"
  }

  secrets = {
    "DB_PW" : aws_ssm_parameter.docdb_password.arn,
    "DB_USER" : aws_ssm_parameter.docdb_username.arn,
  }
}
