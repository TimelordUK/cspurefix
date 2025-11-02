#!/bin/bash
cd /home/jamesste/dev/cs/cspurefix
./PureFix.App/bin/Release/net8.0/PureFix.ConsoleApp \
  --dict Data/FIX44.xml \
  --trim "0,1,2,3,4,5,A,D,8,G,F,9,j,J,P,AE,AK,AU" \
  > test-dictionaries/FIX44-Test.xml
echo "Generated test-dictionaries/FIX44-Test.xml"
wc -l test-dictionaries/FIX44-Test.xml
