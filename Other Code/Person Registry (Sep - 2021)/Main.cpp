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
    tr.T�m();
    PD(tr.L�ggTillTest("olle", "0703955123"));
    PD(tr.L�ggTillTest("olle", "123"));
    PD(tr.L�ggTillTest("kurt�ke", "12345"));
    PD(tr.L�ggTillTest("olle", "456"));
    PD(tr.L�ggTillTest("sven", "456"));
    PD(tr.L�ggTillTest("kurt", "95815"));
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
            reg.L�ggTill(&temp);
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
    reg.T�m();
    reg.Print();
}

void Test2() {
    PersonReg reg(10);
    Init(reg);
    string namn, adress;
    Person te, * tep;

    tep = reg.S�kNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.S�kNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.S�kNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.S�kNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    tep = reg.S�kNamn("olle");
    if (tep) {
        std::cout << tep->Address() << std::endl;
        reg.TaBortEntry(tep);
    }
    else
        std::cout << "not found \n";

    reg.Print();

    reg.T�m();
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
    while (tep = reg.S�kFritt("olle", tep)) {
        tep->Print();
        //        cout << tep->adress << endl;
    }
    std::cout << "not found \n";

    std::cout << "blandade s�kningar \n";
    Person* ptr1 = nullptr, * ptr2 = nullptr;
    bool first = true;
    while (first || ptr1 || ptr2) {
        if (ptr1 || first) {
            ptr1 = reg.S�kFritt("olle", ptr1);
            if (ptr1)
                ptr1->Print();
        }
        if (ptr2 || first) {
            ptr2 = reg.S�kFritt("581", ptr2);
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