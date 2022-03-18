#ifndef Registry
#define Registry
#include <string>

using std::string;

class Person
{
protected:
	string name;
	string address;
public:
	Person();
	Person(const string&, const string&);
	virtual ~Person(){}

	string Name() const;
	string Address() const;

	virtual void Print() const;
};

class PersonMedTel : public Person
{
private:
	string nummer;
public:
	PersonMedTel();
	PersonMedTel(const string&, const string&, const string&);
	~PersonMedTel(){}

	void Print() const;
};

class PersonReg
{
private:
	Person* personer;
	int storlek;
	int kapacitet;

public:
	PersonReg(int maxStorlek);

	~PersonReg();

	bool LäggTill(const Person* const);
	bool LäggTillTest(const string& namn, const string& adress);

	void TaBortEntry(Person* ptr);

	Person* SökNamn(const string& namn) const;
	Person* SökFritt(const string& sökEfter, Person* startOnNext) const;

	void Print() const;

	void Töm();
};

#endif