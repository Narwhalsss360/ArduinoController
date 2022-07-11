#include "cfg.h"
#include <NDefs.h>
#include <TypedByteArray.h>
#include <NStreamCom.h>

auto boardUptime = TypedByteArray<uint32_t, sizeof(uint32_t)>();
NStreamCom serialCom = NStreamCom(&Serial);

void serialEvent()
{
#ifdef USE_LED_BUILTIN
	digitalWrite(LED_BUILTIN, HIGH);
#endif

	NStreamData receivedData = serialCom.parse();
	if (!INVLAID_DATA(receivedData))
	{
		switch ((IN_COM_IDs)receivedData.id)
		{
		case ID_UPTIME_REQUEST:
		{
			serialCom.send<uint32_t, sizeof(uint32_t)>(ID_UPTIME_REQUEST, boardUptime);
			break;
		}
		case ID_PIN_CONTROL:
		{
			switch ((PIN_CONTROLLER_MODES)receivedData.data[PIN_CONTROLLER_MODE_SELECT])
			{
			case MODE_PINMODE:
			{
				uint8_t pinModePinSelect = receivedData.data[PIN_SELECT];
				uint16_t pinModeValue = reinterpret_c_style(uint16_t, &receivedData.data[PIN_CONTROL_VALUE_BYTES]);
				pinMode(pinModePinSelect, pinModeValue);
				break;
			}
			case MODE_DIGITALREAD:
			{
				uint8_t digitalReadPinSelect = receivedData.data[PIN_SELECT];
				byte digitalReadOutBytes[3] = { digitalReadPinSelect, 0, 0 };
				auto digitalReadOutValue = TypedByteArray<uint16_t, sizeof(uint16_t)>();
				digitalReadOutValue = digitalRead(digitalReadPinSelect);
				digitalReadOutBytes[1] = digitalReadOutValue.bytes[0];
				digitalReadOutBytes[2] = digitalReadOutValue.bytes[1];
				serialCom.send(ID_PIN_CONTROL_OUT, &digitalReadOutBytes, OUTCOMIDSIZES[ID_PIN_CONTROL_OUT]);
				break;
			}
			case MODE_DIGITALWRITE:
			{
				uint8_t digitalWritePinSelect = receivedData.data[PIN_SELECT];
				uint16_t digitalWritevalue = reinterpret_c_style(uint16_t, &receivedData.data[PIN_CONTROL_VALUE_BYTES]);
				digitalWrite(digitalWritePinSelect, digitalWritevalue);
				break;
			}
			case MODE_ANALOGREAD:
			{
				uint8_t analogReadPinSelect = receivedData.data[PIN_SELECT];
				byte analogReadOutBytes[3] = { analogReadPinSelect, 0, 0 };
				auto analogReadOutValue = TypedByteArray<uint16_t, sizeof(uint16_t)>();
				analogReadOutValue = analogRead(analogReadPinSelect);
				analogReadOutBytes[1] = analogReadOutValue.bytes[0];
				analogReadOutBytes[2] = analogReadOutValue.bytes[1];
				serialCom.send(ID_PIN_CONTROL_OUT, &analogReadOutBytes, OUTCOMIDSIZES[ID_PIN_CONTROL_OUT]);
				break;
			}
			case MODE_ANALOGWRITE:
			{
				uint8_t analogWritePinSelect = receivedData.data[PIN_SELECT];
				uint16_t analogWriteValue = reinterpret_c_style(uint16_t, &receivedData.data[PIN_CONTROL_VALUE_BYTES]);
				analogWrite(analogWritePinSelect, analogWriteValue);
				break;
			}
			default:
			{
#ifdef USE_LED_BUILTIN
				digitalWrite(LED_BUILTIN, LOW);
				digitalWrite(LED_BUILTIN, HIGH);
#endif
			}
				break;
			}
			break;
		}
		default:
			break;
		}
	}

#ifdef USE_LED_BUILTIN
	digitalWrite(LED_BUILTIN, LOW);
#endif
}

void setup()
{
#ifdef USE_LED_BUILTIN
	pinMode(LED_BUILTIN, OUTPUT);
	digitalWrite(LED_BUILTIN, HIGH);
#endif
	Serial.begin(SERIALCOM_BAUD);
	while (!Serial);
#ifdef USE_LED_BUILTIN
	digitalWrite(LED_BUILTIN, LOW);
#endif
}

void loop()
{
	boardUptime = millis();
}