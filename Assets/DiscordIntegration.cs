using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DiscordController))]
public class DiscordIntegration : MonoBehaviour {

    private DiscordController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<DiscordController>();
        SceneManager.activeSceneChanged += onSceneChanged;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void onSceneChanged(Scene oldScene, Scene newScene) {
        changePresenceDetails("In the "+newScene.name);
    }

    public void onDiscordConnect() {
        controller.presence.details = "In the Main Menu";

        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        long cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalSeconds;

        controller.presence.startTimestamp = cur_time;
        DiscordRpc.UpdatePresence(ref controller.presence);
    }

    public void changePresenceDetails(string details) {
        controller.presence.details = details;
        DiscordRpc.UpdatePresence(ref controller.presence);
    }

    public void changePresenceState(string state)
    {
        controller.presence.state = state;
        DiscordRpc.UpdatePresence(ref controller.presence);
    }

}
