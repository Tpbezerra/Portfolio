#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>
#ifndef DBG_NEW
#define DBG_NEW new ( _NORMAL_BLOCK , __FILE__ , __LINE__ )
#define new DBG_NEW
#endif
#endif

#include <iostream>
#include "PersonReg.h"

#if 1
#define PN(x) std::cout << x
#define DN(x) x
#define PD(x) std::cout << x
#else
#define PN(x)
#define DN(x)
#define PD(x) (x)
#endif

void Init(PersonReg& tr) {
    tr.Töm();
    PD(tr.LäggTillTest("olle", "0703955123"));
    PD(tr.LäggTillTest("olle", "123"));
    PD(tr.LäggTillTest("kurtåke", "12345"));
    PD(tr.LäggTillTest("olle", "456"));
    PD(tr.LäggTillTest("sven", "456"));
    PD(tr.LäggTillTest("kurt", "95815"));
    PN(std::endl);
    PN(("fullt reg "));
    PN((std::endl));
    DN((tr.Print()));
}

#include <fstream>
bool ReadReg(PersonReg& reg, string fileName) {
    string line;
    std::ifstream myfile(fileName);

    if (myfile.is_open())
    {
        while (getline(myfile, line))
        {
            while (line.length() == 0 && getline(myfile, line))
                ;
            string name(line);
            string adress;
            getline(myfile, adress);
            Person temp = Person(name, adress);
            reg.LäggTill(&temp);
        }
        myfile.close();
        return true;
    }
    else {
        std::cout << "Unable to open file";
        return false;
    }
}

void Test1() {
    PersonReg reg(10);
    ReadReg(reg, "PersonExempel.txt");
    reg.Print(); std::cout << "\n\n";
    reg.Töm();
    reg.Print();
}

void Test2() {
    PersonReg reg(10);
    Init(reg);
    string namn, adress;
    Person te, * tep;

    tep = reg.SökNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.SökNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.SökNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.SökNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.SökNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    reg.Print();

    reg.Töm();
    reg.Print();
}

void Test3() {
    PersonReg reg(10);

    Init(reg);
    std::cout << std::endl;
    reg.Print();
    std::cout << std::endl;
    string namn, adress;
    Person te, * tep;

    tep = nullptr;
    while (tep = reg.SökFritt("olle", tep)) {
        tep->Print();
        //        cout << tep->adress << endl;
    }
    std::cout << "not found \n";

    std::cout << "blandade sökningar \n";
    Person* ptr1 = nullptr, * ptr2 = nullptr;
    bool first = true;
    while (first || ptr1 || ptr2) {
        if (ptr1 || first) {
            ptr1 = reg.SökFritt("olle", ptr1);
            if (ptr1)
                ptr1->Print();
        }
        if (ptr2 || first) {
            ptr2 = reg.SökFritt("581", ptr2);
            if (ptr2)
                ptr2->Print();
        }
        first = false;
    }
}

int main() {
    _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
    std::locale::global(std::locale("swedish"));
    PersonReg pr(5);
    Init(pr);
    std::cout << std::endl << "--- Test 1 ---" << std::endl;
    Test1();
    std::cout << std::endl << "--- Test 2 ---" << std::endl;
    Test2();
    std::cout << std::endl << "--- Test 3 ---" << std::endl;
    Test3();
    std::cin.get();
}