String inputString = "";
boolean stringComplete = false;
String commandString = "";
boolean isConnected = false;

int LedPin = 7;

void setup()
{
  pinMode(LedPin, OUTPUT);
  Serial.begin(9600);
}

void serialEvent() {
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    inputString += inChar;
    if (inChar == '~') {
      stringComplete = true;
    }

  }
}
void getCommand()
{
  commandString = inputString.substring(1, 3);
  if (commandString == "ON") {
    digitalWrite(LedPin, HIGH);
  } else if (commandString == "OF") {
    digitalWrite(LedPin, LOW);
  } else if (commandString == "Check") {
    Serial.print("Recieved");
  }

}
void loop()
{
  if (stringComplete)
  {
    stringComplete = false;
    getCommand();
    inputString = "";
  }

}
