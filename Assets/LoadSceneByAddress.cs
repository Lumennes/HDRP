using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

internal class LoadSceneByAddress : MonoBehaviour
{
    public string key; // address string
    private AsyncOperationHandle<SceneInstance> loadHandle;
    [SerializeField] TMP_Text loadText;
    [SerializeField] Canvas loadCanvas;
    bool loading;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(loadCanvas);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(key));
    }

    IEnumerator LoadSceneAsync(string key)
    {
        loading = true;

        loadHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Single, false);

        yield return loadHandle;

        //One way to handle manual scene activation.
        if (loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            loading = false;
            if(loadText)
                loadText.text = $"Loading: 100%";
            yield return loadHandle.Result.ActivateAsync();
            loadCanvas.enabled = false;
        }
    }

    private void Update()
    {
        if (loading && loadText)
            loadText.text = $"Loading: {(int)(loadHandle.PercentComplete*100)}%";
    }

    void OnDestroy()
    {
        Addressables.UnloadSceneAsync(loadHandle);
    }
}