#!/bin/bash

# Replace db user
sed -i "s/__DB_USER__/$DB_USER/" appsettings.template.json

# Replace db password
repl=$(sed -e 's/[&\\/]/\\&/g; s/$/\\/' -e '$s/\\$//' <<<"$DB_PW")
sed -i "s/__DB_PW__/$repl/" appsettings.template.json

# Replace db host
sed -i "s/__DB_HOST__/$DB_HOST/" appsettings.template.json

# Replace db host
sed -i "s/__DB_PORT__/$DB_PORT/" appsettings.template.json

# Replace db params
repl=$(sed -e 's/[&\\/]/\\&/g; s/$/\\/' -e '$s/\\$//' <<<"$DB_PARAMS")
sed -i "s/__DB_PARAMS__/$repl/" appsettings.template.json

cp appsettings.template.json appsettings.json

dotnet CsetAnalytics.Api.dll