String inputString = "";
boolean stringComplete = false;
String commandString = "";
boolean isConnected = false;

int LedPin = 7;

int VRx = A0;
int VRy = A1;

int xPosition = 0;
int yPosition = 0;
int mapX = 0;
int mapY = 0;

void setup()
{
  pinMode(LedPin, OUTPUT);
  pinMode(VRx, INPUT);
  pinMode(VRy, INPUT);
  Serial.begin(38400);
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
  Serial.print("Recieved");
  commandString = inputString.substring(1, 3);
  if (commandString == "ON") {
    digitalWrite(LedPin, HIGH);
  } else if (commandString == "OF") {
    digitalWrite(LedPin, LOW);
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
  xPosition = analogRead(VRx);
  yPosition = analogRead(VRy);
  mapX = map(xPosition, 0, 1023, -512, 512);
  mapY = map(yPosition, 0, 1023, -512, 512);

  Serial.print("X: ");
  Serial.print(mapX);
  Serial.print(" | Y: ");
  Serial.print(mapY);

}
