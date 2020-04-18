/*
	Last edit:2020.2.9
	Author:赖朴然
	E-mail:1696095803@qq.com
	GitHub:Laipuran
*/

#include<iostream>
#include<iomanip>
#include<fstream>
#include<sstream>
#include<algorithm>
#include<cstring>
#include<cstdio>
#include<ctime>
#include<cstdlib>

#include<windows.h>

using namespace std;

void instructions();
void show();

const string PASSWORD = "070714";
int a, j, k, i, r, ct = 0;
int arr[55] = {};
int _count;
char m[200] = {};
string in = "", n = "";

int getrand(int a, int b) {
	srand(time(0)); //随机种子
	int x;
	x = rand() % (b - a + 1) + a;
	return x;
}

void show() {
	system("cls");
	system("color F1");
	cout << setw(50) << "抽奖程序" << setw(55) << "Copyright by 赖朴然 [Version 2.0]" << endl;
	cout << setw(60) << "重启程序请输入-1，关闭程序请输入0。" << endl;
	cout << "请输入人数：";
}

void instructions() {
	cout << setw(60) << "使用说明";
	cout << "重启程序请输入-1，关闭程序请输入0。" << endl;
	cout << "输入一个小于班级人数的数字，将输出一些随机数。" << endl;
	cout << "输入记事本模式以进入记事本模式，此时你可以输入文本，按Enter键确定。" << endl;
	cout << "输入打开记事本以查看记事本的内容。" << endl;
	cout << "\n\n\n还有彩蛋等你来试！\n\n\n";
	system("pause");
}

int main()
{
	system("cls");
	show();
	for (i = 54; i >= 0; i--)arr[i] = 0;
	a = i = j = r = k = ct = 0;
	cin >> in;
	cout << endl;
	if (in == "打开记事本") {
		ifstream in;
		in.open("NotePad.lpr");
		if (!in) {
			cout << "打开失败！" << endl;
			main();
		}
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
		if (!out) {
			cout << "打开失败！" << endl;
			main();
		}
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
		system("cls");
		show();
		cout << "中奖人是汤子欧小姐姐!" << endl;
		system("pause");
		main();
	}
	else if (i == 0) {
		return 0;
	}
	else if (i == -1) {
		system("start 抽奖.exe");
		return 1;
	}
	else if (i > 55 || i < 1) {
		cout << "人数不在范围内！！！" << endl;
		system("pause");
		system("cls");
		main();
	}
	else if (i == 55) {
		cout << endl << endl << "以下为抽到的幸运观众：" << endl << endl;
		for (int z = 1; z <= 55; z++)cout << "中奖号码是" << z << "号" << endl;
		system("pause");
		main();
	}
	if (i < 23) {
		cout << "以下为测试数据，不用看" << endl;
		while (j <= i - 1) {
			r = getrand(55, 1000);
			cout << r << " ";
			a = getrand(1, r);
			while (a >= 56)a -= 55;
			while (a <= 0)a += 55;
			ct = j - 1;
			while (ct >= 0) {
				if (arr[ct] != a)
					ct -= 1;
				else
					a = getrand(1, r);
				while (a >= 56)a -= 55;
				while (a <= 0)a += 55;
			}
			arr[j] = a;
			j++;
			Sleep((int)r * a);
		}
		cout << endl << endl << "以下为抽到的幸运观众：" << endl << endl;
		sort(arr, arr + i);
		for (int z = 0; z <= i - 1; z++)cout << z + 1 << "     中奖号码是" << arr[z] << "号" << endl;
		system("pause");
		main();
	}
	else {
		cout << "以下为测试数据，不用看" << endl;
		while (j < 55 - i) {
			r = getrand(55, 1000);
			cout << r << " ";
			a = getrand(1, r);
			while (a >= 56)a -= 55;
			while (a <= 0)a += 55;
			ct = j - 1;
			while (ct >= 0) {
				if (arr[ct] != a)
					ct -= 1;
				else {
					a = getrand(1, r);
					while (a >= 56)a -= 55;
					while (a <= 0)a += 55;
				}
			}
			arr[j] = a;
			j++;
			Sleep((int)r* a);
		}
		cout << endl << endl << "以下为抽到的幸运观众：" << endl << endl;
		sort(arr, arr + i);
		ct = 0;
		int temp[55];
		for (int i = 0; i < 55; i++) {
			temp[i] = 0;
		}
		for (int i = 0; i < 55; i++) {
			ct = arr[i];
			temp[ct] = ct;
		}
		_count = 1;
		for (int i = 0; i < 55; i++) {
			if (temp[i] == 0) {
				cout << _count << "     中奖号码是" << i + 1 << "号" << endl;
				_count++;
			}
		}
		int z = 0;
		system("pause");
		main();
	}
}
