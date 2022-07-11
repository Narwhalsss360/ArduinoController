# ArduinoController

# Computer Receiving Protocol Defenition
  ID | Description |
  |---|---|
  | 1 | [Uptime](#Computer-Uptime) |
  | 2 | [Pin Control](#Computer-Pin-Control) |

# Computer-Uptime
  **Size: 4 Bytes**
  Byte | Description |
  |---|---|
  | 0 | Board Uptime Bytes `little` |
  | 1 | Board Uptime Bytes `little` |
  | 2 | Board Uptime Bytes `little` |
  | 3 | Board Uptime Bytes `little` |

# Computer-Pin Control
  **Size: 3 Bytes**
  *Only gets returned when a pin value is requested.*
  Byte | Description |
  |---|---|
  | 0 | Pin |
  | 1 | Value Bytes `little` |
  | 2 | Value Bytes `little` |


  --------------------


# Board Receiving Protocol Defenition
  ID | Description |
  |---|---|
  | 1 | [Uptime](#Board-Uptime) |
  | 2 | [Pin Control](#Board-Pin-Control) |

# Board-Uptime
  *This ID is used just as a request handler*
  **Size: 1 Byte ingored**
  Byte | Description |
  |---|---|
  | 0 | Ignored |

# Board-Pin Control
  **Size: 4 Bytes**
  Byte | Description |
  |---|---|
  | 0 | [Pin controller mode](#Pin-Controller-Modes) |
  | 1 | Pin |
  | 2 | Value Bytes `little` |
  | 3 | Value Bytes `little` |

## Board-Pin Controller Modes
  - `pinMode()`
  - `digitalRead()` Ignores `Value` byte. Sends the read value back.
  - `digitalWrite()`
  - `analogRead()` Ignores `Value` byte. Sends the read value back.
  - `analogWrite()`