// readpts.cpp : �������̨Ӧ�ó������ڵ㡣
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
void StringSplit(string s,char splitchar,vector<string>&vec)   //����һ���ַ�����Ϻ���StringSplit(),���ַ���s����splitcharΪ��־���д��
{
	int pos=0;
	while(s.length()>0)
	{
		pos=s.find(splitchar,0);   //���ַ���s �±�0��ʼ������splitchar ����λ�÷��ظ�pos
		Point p;
//		vec.push_back(p);
		vec.push_back(s.substr(0,pos));  //ȡ��s���±��0��pos֮������ַ���
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



