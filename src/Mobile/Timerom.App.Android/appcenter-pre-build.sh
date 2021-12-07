#!/usr/bin/env bash

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/src/Mobile/Timerom.App/AppConstant.cs
echo "To aqui"
echo $APP_CONSTANT_FILE

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating AppCenter secret"
    sed -i 's/Android_AppCenterSecret = "SecretHere"/Android_AppCenterSecret = "'$APPCENTERSECRET'"' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi