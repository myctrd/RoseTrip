@echo off
cd .\tools

cmd /c excelToLua.py
if %errorlevel% equ 0 (
	echo "                       "
	echo "                       "
	echo " LUAȫ�����ɳɹ� "
	echo " ��������Զ����LUA����"
	echo " ע�⣺����ֱ�ӹرձ����ڣ�������"
	pause>nil
	cmd /c copy_to_luadata.bat
	exit
)
color fc
title ����
echo "             �������������ʱ������             "
echo "             �������������ʱ������             "
echo "             �������������ʱ������             "
echo "                 �����Ϸ���ʾ�޸���               "
pause