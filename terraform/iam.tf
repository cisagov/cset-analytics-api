#==============================================
# ECS Assume Role Policy
#
#   This is the document that allows the
#   tasks to assume a role in IAM
#==============================================

#
data "aws_iam_policy_document" "ecs_assume_role" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ecs-tasks.amazonaws.com"]
    }
  }
}


#==============================================
# ECS Execution Role
#
#   This is the role that allows the tasks to
#   write to cloudwatch, pull down container
#   and retrieve parameters from SSM
#==============================================
resource "aws_iam_role" "ecs_execution" {
  name               = "${var.app}-${var.env}-ecs-execution"
  assume_role_policy = data.aws_iam_policy_document.ecs_assume_role.json
}

data "aws_iam_policy_document" "ecs_execution" {
  statement {
    actions = [
      "logs:*",
      "ssm:Get*"
    ]
    resources = ["*"]
  }
}

resource "aws_iam_policy" "ecs_execution" {
  name        = "${var.app}-${var.env}-ecs-execution"
  description = "Policy for ecs execution"
  policy      = data.aws_iam_policy_document.ecs_execution.json
}

resource "aws_iam_role_policy_attachment" "ecs_execution_base" {
  role       = aws_iam_role.ecs_execution.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}

resource "aws_iam_role_policy_attachment" "ecs_execution_other" {
  role       = aws_iam_role.ecs_execution.name
  policy_arn = aws_iam_policy.ecs_execution.arn
}


#==============================================
# ECS Task Role
#
#   This is the role that the actual task uses.
#   This is where additional permissions for
#   services like s3/cognito would be set.
#==============================================
data "aws_iam_policy_document" "api" {
  statement {
    actions = [
      "s3:*",
      "cognito:*"
    ]

    resources = [
      "*"
    ]
  }
}

resource "aws_iam_role" "ecs_task" {
  name               = "${var.app}-${var.env}-ecs-task"
  assume_role_policy = data.aws_iam_policy_document.ecs_assume_role.json
}

resource "aws_iam_policy" "ecs_task" {
  name        = "${var.app}-${var.env}-ecs-task"
  description = "Policy for running ecs tasks"
  policy      = data.aws_iam_policy_document.api.json
}

resource "aws_iam_role_policy_attachment" "ecs_task" {
  role       = aws_iam_role.ecs_task.name
  policy_arn = aws_iam_policy.ecs_task.arn
}
