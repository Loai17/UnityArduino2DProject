
// Joystick
const int joyButtonPin = 7;
int JoyStick_X = A0;
int JoyStick_Y = A1;
int buttonPin = A2;
int buzzerPin = A3;

// Flame
int fDigitalPin = 2;
int fAnalogPin = A4;
int fDigitalVal;
int analogVal;

// RGB LED
int bluepin = 13;
int greenpin = 12;
int redpin = 11;
int rgbVal;

// Rotating Sensor
int RCounter = 0;
boolean Direction;
int Pin_clk_Last;
int Pin_clk_Aktuell;
int pin_clk = 9;
int pin_dt = 8;
int button_pin = 6;

// Card Reader Sensor
const int cardPin = 3;

int Flame = HIGH;
int flameNum = 0;
int knockVal = LOW;
int JbuttonVal = LOW;
int TbuttonVal = LOW;
int RbuttonVal = LOW;
int CardVal = LOW;

int incomingByte[2];

void setup()
{
  pinMode(cardPin, INPUT);
  
  pinMode(redpin, OUTPUT);
  pinMode(bluepin, OUTPUT);
  pinMode(greenpin, OUTPUT);
  
  pinMode(fDigitalPin, INPUT);
  
  pinMode(joyButtonPin, INPUT);

  digitalWrite(joyButtonPin, HIGH);

  pinMode (JoyStick_X, INPUT);
  pinMode (JoyStick_Y, INPUT);
  pinMode(buttonPin,INPUT);
  pinMode(buzzerPin,OUTPUT);


  // input pins are initialized ...
  pinMode (pin_clk, INPUT);
  pinMode (pin_dt, INPUT);
  pinMode (button_pin, INPUT);
    
  // ... and their pull-up resistors activated
  digitalWrite (pin_clk, true);
  digitalWrite (pin_dt, true);
  digitalWrite (button_pin, true);
  
  // Initial reading of the Pin_CLK
  Pin_clk_Last = digitalRead (pin_clk);
  
  Serial.begin(9600);
}

void loop()
{
  // Card Reader Logic
  CardVal = digitalRead(cardPin);
  
  // Rotation Logic

  // Read out the current status
   Pin_clk_Aktuell = digitalRead (pin_clk);
// Check for change
   if (Pin_clk_Aktuell != Pin_clk_Last)
   {
          
        if (digitalRead (pin_dt) != Pin_clk_Aktuell)
        {
            // Pin_CLK changed first
            RCounter ++;
            Direction = true;
        }
          
        else
        {// Otherwise, Pin_DT changed first
            Direction = false;
            RCounter--;
        }
//        Serial.println ("Rotation detected:");
//        Serial.print ("Direction of rotation:");
//         
        if (Direction)
        {
//           Serial.println ("clockwise");
        }
        else
        {
//           Serial.println ("counterclockwise");
        }
         
//        Serial.print ("Current position:");
//        Serial.println (Counter);
//        Serial.println ("------------------------------");
          
   }
    
   // Preparation for the next run:
   // The value of the current run is the previous value for the next run
   Pin_clk_Last = Pin_clk_Aktuell;
    
   // Reset function to save the current position
   if (!digitalRead(button_pin))
   {
     RbuttonVal = HIGH;
   }
   else {
     RbuttonVal = LOW; 
   }



  // Rotation logic until here ========





  
  int flame = 0;
  
  // Flame Sensor Logic
  fDigitalVal = digitalRead(fDigitalPin);
  if(fDigitalVal == HIGH)
  {
      flame = 1;
      digitalWrite (bluepin, LOW);
      digitalWrite (greenpin, LOW); 
      digitalWrite (redpin, HIGH);
      
  }
  else
  {
    flame = 0;
    digitalWrite (redpin, LOW);
  }
//  Serial.println(fDigitalVal);
  analogVal = analogRead(fAnalogPin);
//  Serial.println(fAnalogPin);
  // delay(100);
  

  // Joystick Logic
  float x, y;
 
  // Sensor Reading
//  flameNum = analogRead(flameDataPin);
//  knockVal = digitalRead(knockPin);  
//  Flame = digitalRead(flamePin);
  JbuttonVal = digitalRead(joyButtonPin);
  TbuttonVal = digitalRead(buttonPin);

  if(JbuttonVal == HIGH){
    noTone(buzzerPin); 
  }
  else if(JbuttonVal == LOW)
  {
    tone(buzzerPin,100);
  }

  x = analogRead (JoyStick_X) * (5.0 / 1023.0);
  y = analogRead (JoyStick_Y) * (5.0 / 1023.0);


  if(RbuttonVal == HIGH || JbuttonVal == LOW || CardVal == HIGH){
      digitalWrite (greenpin, HIGH);  
      digitalWrite (bluepin, LOW); 
  }

  else if(x<=2.7 && x>=2.5 && y<=2.7 && y>=2.5 && fDigitalVal == LOW && RbuttonVal == LOW && JbuttonVal == HIGH && CardVal == LOW) {
      digitalWrite (greenpin, LOW);
      digitalWrite (bluepin, HIGH);  
          
  }
  else if(fDigitalVal == LOW){
      digitalWrite (greenpin, HIGH);  
      digitalWrite (bluepin, LOW);    
  }
  // 0, 1, 2,       3,           4,                5,             6
  // X, Y, JButton, FlameSensor, Rotating Counter, Rotary Button, Card Reader
  Serial.print (x, 4);  Serial.print (","); Serial.print (y, 4);Serial.print(", "); 
  Serial.print(JbuttonVal); Serial.print(", "); Serial.print (flame); 
  Serial.print(", "); Serial.print(RCounter); Serial.print(", "); Serial.print(RbuttonVal); 
  Serial.print(", "); Serial.println(CardVal);

//  if (Serial.available() > 0) {
//    while (Serial.peek() == 'L') {
//      Serial.read();
//      incomingByte[0] = Serial.parseInt();
//      if (incomingByte[0] == 1) { LED = true; } else { LED = false; }
//    }
//   } 
//   else {
//      
//   }
//    while (Serial.available() > 0) {
//      Serial.read();
//    }
//  ledCheck(); 

  // Taster Modul Logic


//  // RGB
//  for(rgbVal = 255; rgbVal > 0; rgbVal--)
//  {
//    analogWrite(redpin, rgbVal);
//    analogWrite(bluepin, 255 - rgbVal);
//    analogWrite(greenpin, 128 - rgbVal);
//    Serial.println(rgbVal, DEC);
//    delay(5); 
//  }
//  for(rgbVal = 0; rgbVal < 255; rgbVal++)
//  {
//    analogWrite(redpin, rgbVal);
//    analogWrite(bluepin, 255 - rgbVal);
//    analogWrite(greenpin, 128 - rgbVal);
//    
//    Serial.println(rgbVal, DEC);
//    delay(5); 
//  }
  
  
 delay(100);

 
} 
