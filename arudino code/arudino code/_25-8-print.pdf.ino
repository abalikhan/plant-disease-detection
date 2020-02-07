int incomingByte = 0;   // for incoming serial data

void setup() {
        Serial.begin(9600);     // opens serial port, sets data rate to 9600 bps
  pinMode(13, OUTPUT);
}

void loop() {

        // send data only when you receive data:
        if (Serial.available() > 0) {
                // read the incoming byte:
                incomingByte = Serial.read();
                Serial.print(incomingByte);
                
    if (incomingByte == 49)
    {
        digitalWrite(13, HIGH);   // turn the LED on (HIGH is the voltage level)
        delay(5000);              // wait for a second
        digitalWrite(13, LOW);    // turn the LED off by making the voltage LOW
        delay(5000);
    }
                // say what you got:
                //Serial.print("I received: ");
                //Serial.println(incomingByte, DEC);
        }
}
 
