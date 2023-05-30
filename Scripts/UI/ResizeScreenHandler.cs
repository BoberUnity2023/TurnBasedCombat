namespace DNVMVC
{
	using UnityEngine;

	public class ResizeScreenHandler : MonoBehaviour
	{
		public delegate void ResizeScreenHandlerDelegate(Vector2 aScreenSize, float aScale);
		public event ResizeScreenHandlerDelegate EventResized;


		public int defaultScreenWidth = 828;
		public int defaultScreenHeight = 1792;


		private UIRootCanvas _uiRoot;
		private int _prevWidth;
		private int _prevHeight;
		private float _width;
		private float _height;
		private float _scale;


		public float Width { get => _width; }
		public float Height { get => _height; }
		public float Scale { get => _scale; }


		private void Awake()
		{
			_uiRoot = GameObject.FindObjectOfType<UIRootCanvas>();
			if (_uiRoot == null)
			{
				Debug.LogError("Can't find UIRoot on the scene!");
			}

			_width = defaultScreenWidth;
			_height = defaultScreenHeight;
			_prevWidth = -1;
			_prevHeight = -1;
		}

		private void LateUpdate()
		{
			if (_prevWidth != Screen.width || _prevHeight != Screen.height)
			{
				float ratio = (float)_uiRoot.ActiveHeight / Screen.height;
				_width = Mathf.Ceil(Screen.width * ratio);
				_height = Mathf.Ceil(Screen.height * ratio);
				_scale = _width / defaultScreenWidth;

				EventResized?.Invoke(new Vector2(_width, _height), _scale);

				_prevWidth = Screen.width;
				_prevHeight = Screen.height;
			}
		}

		public void OnResizeEvent(ResizeScreenHandlerDelegate aCallback)
		{
			EventResized += aCallback;
		}
	}
}
