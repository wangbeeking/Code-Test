#include <iostream>
using namespace std;

class INT
{
public:
	INT::INT(int i):m_int(i){}
	INT& INT::operator++()
	{
		++(this->m_int);
		return *this;
	}
	const INT INT::operator++(int)
	{
		INT tempINT(*this);
		++(this->m_int);
		return tempINT;
	}	
	INT& INT::operator--()
	{
		--(this->m_int);
		return *this;
	}
	const INT INT::operator--(int)
	{
		INT tempINT(*this);
		--(this->m_int);
		return tempINT;
	}
	int & INT::operator*()
	{
		return (int&)(this->m_int);
	}
	friend ostream& operator<<(ostream& os,const INT& I);
private:
	int m_int;
};
ostream& operator<<(ostream& os,const INT& I)
{
	os<<"["<<I.m_int<<"]"<<endl;
	return os;
}

int main()
{
	INT I(5);
	cout<<I++<<endl;
	cout<<++I<<endl;
	cout<<I--<<endl;
	cout<<--I<<endl;
	cout<<*I<<endl;
	system("pause");
	return 0;
}