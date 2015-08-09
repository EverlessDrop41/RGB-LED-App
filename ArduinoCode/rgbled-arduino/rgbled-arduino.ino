const int redPin = 11;
const int bluePin = 10;
const int greenPin = 9;

int redOutValue = 64;
int greenOutValue = 64;
int blueOutValue = 64;

//Passed in via REDVAL:GREENVAL:BLUEVAL
//E.G. 1:115:15

// Maximum number of substrings expected
const int MAX_SUBSTRINGS = 3;
const char COLON = ':';

void setup(){
  pinMode(redPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  pinMode(greenPin, OUTPUT);

  Serial.begin(9600);
}

void loop(){
  String content = "";
  while (Serial.available() > 0) {
    char incoming = Serial.read();
    content.concat(incoming);
    delay (10);
  }
  
  String substrings[MAX_SUBSTRINGS];

  int firstColonIndex = content.indexOf(COLON);
  int secondColonInex = content.indexOf(COLON, firstColonIndex + 1);

  if (content != "") {
    substrings[0] = content.substring(0, firstColonIndex);
    substrings[1] = content.substring(firstColonIndex + 1, secondColonInex);
    substrings[2] = content.substring(secondColonInex + 1);
  }

  //Serial.println(substrings[1].toInt());

  redOutValue = content != "" ? substrings[0].toInt() : redOutValue;
  greenOutValue = content != "" ? substrings[1].toInt() : greenOutValue;
  blueOutValue = content != "" ? substrings[2].toInt() : blueOutValue;

  analogWrite(redPin, redOutValue);
  analogWrite(greenPin, greenOutValue);
  analogWrite(bluePin, blueOutValue);
}