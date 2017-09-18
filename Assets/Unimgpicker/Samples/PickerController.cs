using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private Image imageRenderer;

        public Image outImagePreview;
        public GameObject panelPreview;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, imageRenderer));
            };
        }

        public void Start()
        {
           // OnPressShowPicker();
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
        }

        private IEnumerator LoadImage(string path, Image output)
        {

            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;
            yield return new WaitForSeconds(0.5f);
            var texture = www.texture;
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }else
            {
                PlayerPrefs.SetString("imagePath-" + Login.debugUser.id, path);
            }
            panelPreview.SetActive(true);
            output.transform.eulerAngles = Vector3.zero;
            outImagePreview.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
            output.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
            Login.UpdateGUI();
        }
    }
}