#include <iostream>
#include <cstdlib>
#include <vector>
#include <numeric>
#include <random>
#include <algorithm>
#include "PersonReg.h"

using std::vector;

void Uppgift1a()
{
	vector<int> v = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
	std::random_shuffle(v.begin(), v.end());

	std::cout << "Osorterad: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");
	}

	std::sort(v.begin(), v.end());

	std::cout << "Sorterad: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");;
	}
}

void Uppgift1b()
{
	size_t length = 20;
	int a[] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
	std::random_shuffle(a, a+length);

	std::cout << "Osorterad: ";
	for (size_t i = 0; i < length; i++)
	{
		std::cout << a[i] << ((i == length - 1) ? "\n" : ", ");
	}

	std::sort(a, a+ length);

	std::cout << "Sorterad: ";
	for (size_t i = 0; i < length; i++)
	{
		std::cout << a[i] << ((i == length - 1) ? "\n" : ", ");;
	}
}

void Uppgift1c()
{
	vector<int> v = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
	std::random_shuffle(v.begin(), v.end());

	std::cout << "Osorterad: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");
	}

	std::sort(v.rbegin(), v.rend());

	std::cout << "Sorterad: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");;
	}
}

void Uppgift1d()
{
	size_t length = 20;
	int a[] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
	std::random_shuffle(a, a + length);

	std::cout << "Osorterad: ";
	for (size_t i = 0; i < length; i++)
	{
		std::cout << a[i] << ((i == length - 1) ? "\n" : ", ");
	}

	struct
	{
		bool operator()(int a, int b) { return a > b; }
	} greater;

	std::sort(a, a + length, greater);

	std::cout << "Sorterad: ";
	for (size_t i = 0; i < length; i++)
	{
		std::cout << a[i] << ((i == length - 1) ? "\n" : ", ");;
	}
}

void InitReg(PersonReg& reg)
{
	reg.LäggTillTest("Bertil", "123");
	reg.LäggTillTest("Bertil", "5125");
	reg.LäggTillTest("Magnus", "8236");
	reg.LäggTillTest("Bertil", "12");
	reg.LäggTillTest("Maria", "897");
	reg.LäggTillTest("Erik", "1654");
	reg.LäggTillTest("Lukas", "6854");
	reg.LäggTillTest("Maria", "165");
	reg.LäggTillTest("Hannah", "6558");
	reg.LäggTillTest("Erik", "1645");
	reg.LäggTillTest("Torre", "1968");
	reg.LäggTillTest("Lukas", "156");
}

void Uppgift2a(PersonReg& reg)
{
	std::random_shuffle(reg.Begin(), reg.End());

	std::cout << "Osorterad:\n";
	reg.Print();

	std::sort(reg.Begin(), reg.End());

	std::cout << "\nSorterad i stigande ordning:\n";
	reg.Print();
}

void Uppgift2b(PersonReg& reg)
{
	std::random_shuffle(reg.Begin(), reg.End());

	std::cout << "Osorterad:\n";
	reg.Print();

	struct
	{
		bool operator()(const Person& a, const Person& b) { return a.Address() > b.Address(); }
	} compare;

	std::sort(reg.Begin(), reg.End(), compare);

	std::cout << "\nSorterad baklänges med adress:\n";
	reg.Print();
}

void Uppgift3()
{
	vector<int> v = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
	std::random_shuffle(v.begin(), v.end());

	std::cout << "Osorterad: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");
	}

	struct
	{
		bool operator()(int a) { return  a % 2 == 0; }
	} even;

	vector<int>::iterator end = std::remove_if(v.begin(), v.end(), even);
	v.erase(end, v.end());

	std::cout << "Utan jämna tal: ";
	for (size_t i = 0; i < v.size(); i++)
	{
		std::cout << v.at(i) << ((i == v.size() - 1) ? "\n" : ", ");
	}
}

int main()
{
	std::locale::global(std::locale("swedish"));

	std::cout << "--- Uppgift 1a ---\n";
	Uppgift1a();
	std::cout << "\n--- Uppgift 1b ---\n";
	Uppgift1b();
	std::cout << "\n--- Uppgift 1c ---\n";
	Uppgift1c();
	std::cout << "\n--- Uppgift 1d ---\n";
	Uppgift1d();

	PersonReg reg(12);
	InitReg(reg);
	std::cout << "\n--- Uppgift 2a ---\n";
	Uppgift2a(reg);
	std::cout << "\n--- Uppgift 2b ---\n";
	Uppgift2b(reg);

	std::cout << "\n--- Uppgift 3 ---\n";
	Uppgift3();
}