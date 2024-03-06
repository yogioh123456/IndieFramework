using System;
using System.Collections.Generic;
using System.Text;

public class RoomManager {
    private Dictionary<string, RoomData> roomDatas = new Dictionary<string, RoomData>(64);

    public RoomManager()
    {
        roomDatas.Clear();
    }
    
    public void CreateRoom(string name, int hostId) {
        if (roomDatas.ContainsKey(name)) {
            Debug.Log("房间名存在");
            return;
        }
        
        RoomData roomData = new RoomData();
        roomData.roomName = name;
        roomData.roomPlayer.Clear();
        roomData.roomPlayer.Add(hostId);
        roomDatas.Add(name, roomData);
    }

    public void ExitRoom(string roomName, int playerId) {
        if (roomDatas.ContainsKey(roomName)) {
            roomDatas[roomName].roomPlayer.Remove(playerId);
            if (roomDatas[roomName].roomPlayer.Count == 0) {
                roomDatas.Remove(roomName);
            }
        }
    }

    public void EnterRoom(string roomName, int playerId) {
        if (roomDatas.ContainsKey(roomName)) {
            if (roomDatas[roomName].roomPlayer.Count < roomDatas[roomName].maxNum) {
                roomDatas[roomName].roomPlayer.Add(playerId);
            } else {
                Debug.Log("房间已满");
            }
        } else {
            Debug.Log("房间不存在");
        }
    }

    public void GetAllRoom() {
        List<RoomData> list = new List<RoomData>(64);
        foreach (var room in roomDatas) {
            list.Add(room.Value);
        }
    }
}

public class RoomData {
    public string roomName;
    public int maxNum = 3;
    public List<int> roomPlayer = new List<int>(3);
}