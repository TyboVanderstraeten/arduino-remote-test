const int LED_PIN = 13;
int ledState = 0; 

void setup() {
  pinMode(LED_PIN, OUTPUT);

  Serial.begin(9600);
}

void loop() {
    char receiveValue;   
   
    if(Serial.available() > 0)
    {        
        receiveValue = Serial.read();
        
       if(receiveValue == '1')    
          ledState = 1;   
       else
          ledState = 0;     
    }   
      
    digitalWrite(LED_PIN, ledState); 
      
    delay(50);    
}
