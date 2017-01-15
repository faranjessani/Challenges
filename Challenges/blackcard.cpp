#include <iostream>
#include <string>
#include <algorithm>
#include <fstream>

using namespace std;

string findSurvivor(vector<string> pirates, int blackSpot);
void printVector(vector<string> vec);

vector<string> split(string s, char delim) {
    vector<string> result;
    size_t oldIndex = 0;
    size_t index = s.find_first_of(delim);
    while(index != string::npos) {
        result.push_back(s.substr(oldIndex, index));
        oldIndex = index;
        index = s.find_first_of(delim);
    }

    printVector(result);
    return result;
}


int main() {
    string fileName;
    cin >> fileName;
    ifstream myFile(fileName, ifstream::in);
    string line;
    if(myFile.is_open()) {
        while(getline(myFile, line)) {
            vector<string> split1 = split(line, '|');
            vector<string> split2 = split(split.at(0), ' ');
            //int blackSpot(split1[1]);
            cout << line << endl;
        }
        myFile.close();
    }
    vector<string> vec(0);
    vec.push_back("John");
    vec.push_back("Sara");
    vec.push_back("Tom");
    vec.push_back("Susan");
    int blackSpot = 3;

    string survivor = findSurvivor(vec, blackSpot);
    cout << "Survivor: " << survivor << endl;
    vec.clear();
    vec.push_back("John");
    vec.push_back("Tom");
    vec.push_back("Mary");
    blackSpot = 5;
    survivor = findSurvivor(vec, blackSpot);
    cout << "Survivor: " << survivor << endl;
}

void printVector(vector<string> vec) {
    for(auto i = vec.begin(); i != vec.end(); ++i) {
        cout << *i << endl;
    }
}

string findSurvivor(vector<string> pirates, int blackSpot) {
    int toErase;
    do {
        printVector(pirates);
        cout << "pirates.size() = " << pirates.size() << endl;
        toErase = blackSpot % pirates.size(); 
        cout << "To erase, &index: " << toErase << endl;
        if(0 == toErase) {
            pirates.pop_back();
        } else {
            pirates.erase(pirates.begin() + toErase - 1);
        }
    } while(pirates.size() > 1);
    return pirates[0];
}
