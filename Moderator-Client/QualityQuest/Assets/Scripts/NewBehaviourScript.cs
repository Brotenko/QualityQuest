using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
using MessageContainer.Messages;

public class NewBehaviourScript : MonoBehaviour
{

    public class Test
    {
        string name;
        int age;

        public Test(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

    }

    public void Start()
    {
        Test test = new Test("fassbendi",12);

        string apfel = JsonConvert.SerializeObject(test);

        Test neuerTest = JsonConvert.DeserializeObject<Test>(apfel);

        var winOpt = new KeyValuePair<Guid,string>(new Guid(), "Nudel");

        var votRes = new Dictionary<KeyValuePair<Guid, string>,int>();

        votRes.Add(new KeyValuePair<Guid,string>(new Guid(),"Nudel"),3);

        var hehe = new VotingEndedMessage(new Guid(),winOpt,votRes);

        Debug.Log("A");
        string nudel = JsonConvert.SerializeObject(test);
        Debug.Log("B");
        //VotingEndedMessage msg = JsonConvert.DeserializeObject<VotingEndedMessage>(nudel);
        Debug.Log("C");
        string affe = JsonConvert.SerializeObject(votRes);
        Debug.Log("D");
        //Dictionary<KeyValuePair<Guid, string>, int> esel = JsonConvert.DeserializeObject<Dictionary<KeyValuePair<Guid, string>, int>>(affe);
        Debug.Log("E");
    }

}
