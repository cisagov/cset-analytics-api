# This contains th e route53 record for the load balancer
resource "aws_route53_record" "domain" {
  zone_id = data.aws_route53_zone.zone.zone_id
  name    = "${var.app}.${var.env}.${data.aws_route53_zone.zone.name}"
  type    = "CNAME"
  ttl     = "300"
  records = [module.alb.alb_dns_name]
}
