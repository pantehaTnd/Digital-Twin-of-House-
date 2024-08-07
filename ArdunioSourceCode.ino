/**
 * Author:    Panteha Taghavinezjad
 * Created:   Spring/Summer of 2024
 * 
 **/


const int bedPin = 13;
const int livingPin =12;
const int buzzPin = 10;
const int buttonPin =11;
const int ldrPin = A5;
const int threshold = 200;

//Toşumuz basılı olmadığı ilk durum olarak alırız
bool buttonState = LOW;
bool lastButtonState = LOW;


void setup() {
   pinMode(bedPin, OUTPUT);
   pinMode(livingPin, OUTPUT);
   pinMode(buzzPin, OUTPUT);
   pinMode(buttonPin,  INPUT);
   pinMode(ldrPin, INPUT);
   Serial.begin(9600);
}

void loop() {
  int ldrValue =analogRead(ldrPin);

  if(ldrValue> threshold){
    Serial.println("Day");

  }else{
    Serial.println("Night");
  }



  buttonState =digitalRead(buttonPin);

  if(buttonState != lastButtonState){
    if (buttonState == HIGH){
      Serial.println("ButtonPressed");
    }
    lastButtonState = buttonState;
  }


   if(Serial.available() > 0){
    char data = Serial.read();
    Serial.print("Received: ");
    Serial.println(data);

    if (data == '1') {
      digitalWrite(bedPin, HIGH); 
      
    } else if (data == '0') {
      digitalWrite(bedPin, LOW); 
      
    }

    if (data == '3') {
      digitalWrite(livingPin, HIGH); 
    } else if (data == '2') {
      digitalWrite(livingPin, LOW); 
    }

    if (data == 'B'){
      Serial.println("Buzzer signali alindi");
      digitalWrite(buzzPin, HIGH); 
      delay(500);
      digitalWrite(buzzPin,  LOW);
    }

   }
  
}
