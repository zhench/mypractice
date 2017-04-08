// readpts.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include<vector>
#include<string>
#include<iostream>
#include<fstream>
#include<sstream>
using namespace std;

struct Point
{
	float x,y,z,I; 
};
void StringSplit(string s,char splitchar,vector<string>&vec)   //定义一个字符串打断函数StringSplit(),把字符串s中以splitchar为标志进行打断
{
	int pos=0;
	while(s.length()>0)
	{
		pos=s.find(splitchar,0);   //从字符串s 下标0开始，查找splitchar ，把位置返回给pos
		Point p;
//		vec.push_back(p);
		vec.push_back(s.substr(0,pos));  //取得s中下标从0到pos之间的子字符串
		for(int i=0;i<vec.size();i++)
		{
			p.x= atof(vec[0].c_str());
			p.y= atof(vec[1].c_str());
			p.z=atof(vec[2].c_str());
			p.I=atof(vec[3].c_str());
			cout<<p.x<<" "<<p.y<<" "<<p.z<<" "<<p.I;

		}
		cout<<endl;
		if(pos<0)
		{
			break;
		}
		s=s.substr(pos+1);
	}
} 
void READFILETOSTRING()
{
	ifstream fin("split.txt");
	string s;
	vector<string>vec;
	while(getline(fin,s))
	{
		StringSplit(s,' ',	vec);
	}
}

int main()
{
	READFILETOSTRING();
	system("pause");
	return 0;
};



