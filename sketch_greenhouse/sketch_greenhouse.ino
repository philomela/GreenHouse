int lampPinOne = 2, 
lampPinTwo = 3, 
lampPinThree = 4, 
waterPin = A1, 
tempPin = A3;

int i = 0;
void setup() {
  
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(lampPinOne, OUTPUT);
  pinMode(lampPinTwo, OUTPUT);
  pinMode(lampPinThree, OUTPUT);
  Serial.begin(9600);
  connectComputer();
}

void loop() {
  
  digitalWrite(LED_BUILTIN, HIGH);
  digitalWrite(lampPinOne, LOW);
  digitalWrite(lampPinTwo, LOW);
  digitalWrite(lampPinThree, LOW);
  delay(20000);
  int valWaterSensor = analogRead(waterPin);
  int valTempSensor = analogRead(tempPin);
  Serial.println(valWaterSensor);
  Serial.println(valTempSensor);
  if (i == 5) {
    i = 0;   
  }
  i++;
  Serial.println(i);
  digitalWrite(LED_BUILTIN, LOW);
  digitalWrite(lampPinOne, HIGH);
  digitalWrite(lampPinTwo, HIGH);
  digitalWrite(lampPinThree, HIGH);
  delay(5000);
}

void connectComputer() {
  volatile int msgIndicator = -1;
  while (msgIndicator == -1) {
    Serial.println("PingMsg");
    msgIndicator = Serial.read();
  }
}
