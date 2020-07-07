# uHue

*A USB-controlled LED indicator*

The uHUE is a simple USB-controlled indicator that arose from my need to add some kind of external indicator to a PC-based test station I'd created. The purpose of the indicator was show from a distance - and from an angle - the status of the test station. The uHUE interfaces with the PC using a USB-to-serial converter (SILICON LABS CP2104) and a simple protocol is used to control the LEDS. 

The colour shown by the uHUE is specified using three 8-bit values - one each for red, green and blue. Three 'patterns' are possible when showing a colour: solid, blink and fade. For blink and fade, the cycle time can also be controlled.

The housing is designed to allow the LED colour being shown to be viewed over a 180 degree horizontal arc and 90 degree vertical arc. I opted to use integrated magnets hold the uHUE in place, rather than a more permanent fixing method.
