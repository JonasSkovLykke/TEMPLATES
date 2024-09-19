@ECHO OFF


docker info > nul 2>&1
IF NOT %ERRORLEVEL% == 0 (
    ECHO Docker is not running on this machine.
    PAUSE
    EXIT /B
)


:MENU
CLS
ECHO.
ECHO ======================================================
ECHO  PRESS 1 OR 2 to select your task, or 0 to EXIT.
ECHO ======================================================
ECHO.
ECHO 1 - Test Build
ECHO 2 - Production Build
ECHO 0 - EXIT
ECHO.
SET /P M=Type 1, 2, or 0 then press ENTER: 


:VERSION
CLS
ECHO.
ECHO ======================================================
ECHO  Enter a version number of the build/image Fx: 1.0.0.
ECHO ======================================================
ECHO.
SET /P V=Type Versions number. Fx: '1.0.0' then press ENTER: 


IF %M% == 1 GOTO TEST
IF %M% == 2 GOTO PRODUCTION
IF %M% == 0 GOTO EOF


:TEST
IF "%V%" == "" GOTO VERSION
CLS
CALL npm version %V%
docker build --rm -t <repository/image-name>:latest -f ./src/Console/Dockerfile .
docker image tag <repository/image-name>:latest <repository/image-name>:%V%
docker push <repository/image-name>:%V%
docker rmi <repository/image-name>:%V%
GOTO EOF


:PRODUCTION
IF "%V%" == "" GOTO VERSION
CLS
CALL npm version %V%
docker build --rm -t <repository/image-name>:latest -f ./src/Console/Dockerfile .
docker image tag <repository/image-name>:latest <repository/image-name>:%V%
docker push <repository/image-name>:%V%
docker rmi <repository/image-name>:%V%
GOTO EOF


:EOF