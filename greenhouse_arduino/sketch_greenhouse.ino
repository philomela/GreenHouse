void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
}

int i = 0;

void loop() {
  digitalWrite(LED_BUILTIN, HIGH);
  delay(5000);
  Serial.println("Hello Kitty!");
  if (i == 5) {
    i = 0;   
  }
  i++;
  Serial.println(i);
  digitalWrite(LED_BUILTIN, LOW);
  delay(1000);
}
