/*
	Last edit:2020.1.15
	Author:赖朴然
	E-mail:1696095803@qq.com
	GitHub:Laipuran
*/

#include<iostream>
#include<fstream>

#include<cstring>
#include<sstream>

#include<iomanip>
#include<algorithm>
#include<cstdio>
#include<ctime>
#include<cstdlib>

using namespace std;

const string PASSWORD = "070714";
int a, j, k, i;
int arr[55];
char m[500];
string in, n;

void reset() {
	a = j = 0;
	m[500] = {};
	n = in = "";
}

void instructions() {
	reset();
	cout << setw(60) << "使用说明";
	cout << "重启程序请输入“重启”，关闭程序请输入“退出”。" << endl;
	cout << "输入一个小于班级人数的数字，将输出一些随机数。" << endl;
	cout << "输入记事本模式以进入记事本模式，此时你可以输入文本，按Enter键确定。" << endl;
	cout << "输入打开记事本以查看记事本的内容。" << endl;
	cout << "\n\n\n还有彩蛋等你来试！\n\n\n";
	system("pause");
}

void show() {
	system("cls");
	system("color F1");
	cout << setw(50) << "抽奖程序" << setw(55) << "Copyright by 赖朴然 [Version 1.6]" << endl;
	cout << setw(60) << "重启程序请输入“重启”，关闭程序请输入“退出”。" << endl;
	cout << "请输入人数：";
}

int main()
{
	show();
	reset();
	system("start osk");
	cin >> in;
	cout << endl;
	if (in == "退出") {
		exit(0);
	}
	else if (in == "重启") {
		system("start choujiang.exe");
		exit(1);
	}
	else if (in == "打开记事本") {
		ifstream in;
		in.open("NotePad.lpr");
		while (in >> m)in >> m;
		cout << m << endl;
		in.close();
		main();
	}
	else if (in == "记事本模式") {
		system("cls");
		ofstream out;
		system("color F4");
		cout << setw(70) << "记事本模式" << endl;
		out.open("记事本.lpr");
		cin >> n;
		out << n;
		out.close();
			cout << "成功!" << endl;
			system("pause");
			system("cls");
		main();
	}
	else if (in == PASSWORD) {
		cout << "管理员模式" << endl;
		system("pause");
		system("cls");
		show();
		cin >> in;
		system("cls");
		show();
		cout << endl << "中奖号码是" << in << "号" << endl;
		system("pause");
			main();
	}
	else {
		stringstream ss;
		ss << in;
		ss >> i;
	}
	if (i == 666) {
		cout << "中奖人是汤子欧小姐姐" << endl;
		system("pause");
		main();
	}
	else if (i > 55) {
		cout << "人数超过55！！！" << endl;
		system("pause");
		system("cls");
		main();
	}
	srand(time(0)); //随机种子
	a = rand()%55 + 1; //随机产生1至55的整数
		cout << "中奖号码是" << a << "号" << endl;
	while (j <= i - 2) {
		k = rand()%1000 + 1;
		while (k > 55)k -= 55;
		arr[j] = k;
		j++;
	}
	sort(arr, arr + i);
	for (int z = 0; z < i; z++)cout << "中奖号码是" << arr[z] << "号" << endl;
	system("pause");
	main();
}
