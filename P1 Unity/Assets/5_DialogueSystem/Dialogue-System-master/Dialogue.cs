using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //visible en el editor
public class Dialogue
{ //no hereda del MonoBehavior ,será llamado por DalogueTrigger, no se asocia a ningún objeto

	[TextArea(1, 1)]
	public string[] name; //personaje que va a hablar
	
	[TextArea(3, 10)] //los text box ocupan como mín 3 y máx 1o líneas de txt
	public string[] sentences; //contiene las frases que se cargarán en el Queue del DM

}
