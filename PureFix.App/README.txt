from vs admin level dev shell

MakeCert -ss Root -sr LocalMachine -a SHA256 -n "CN=127.0.0.1,CN=localhost" -sv local.pvk local.cer -pe -e 12/31/2099 -len 2048
pvk2pfx.exe -pvk local.pvk -spc local.cer -pfx local.pfx

