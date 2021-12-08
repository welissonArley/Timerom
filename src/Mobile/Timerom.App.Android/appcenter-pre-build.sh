#!/usr/bin/env bash

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/Mobile/Timerom.App/AppConstant.cs

if [ -e "$APP_CONSTANT_FILE" ]
then
    sed -i '' 's#Android_AppCenterSecret = "SecretHere"#Android_AppCenterSecret = "'$APPCENTERSECRET'"#' $APP_CONSTANT_FILE
fi