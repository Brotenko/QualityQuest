using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Testing
{
    
    public GameObject testObject;
    public TestScript testScript;
    

    [SetUp]
    public void SetUp()
    {
        testObject = GameObject.Instantiate(new GameObject());
        testScript = testObject.AddComponent<TestScript>();


        testScript.text = new TextMeshPro();
        testScript.harald = GameObject.Instantiate(new GameObject());
    }
    

    
    [UnityTest]
    public IEnumerator Test()
    {
        yield return new WaitForEndOfFrame(); // AWAKE
        Assert.IsTrue(testScript.a == 5);
        yield return new WaitForEndOfFrame(); // Start
        yield return new WaitForEndOfFrame(); // Update
        testScript.SetText();
        Assert.AreEqual("haha", testScript.text.text);
        yield return new WaitForEndOfFrame(); // Update
        testScript.Test();
        Assert.IsTrue(testScript.harald.activeSelf);
    }
}
