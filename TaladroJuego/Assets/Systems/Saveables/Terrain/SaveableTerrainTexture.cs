using Newtonsoft.Json;
using SaveSystem.Saveable;
using System;
using System.Linq;
using System.Runtime.Serialization;
using TerrainSystem.Requester;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace SaveableSystem.TerrainSystem
{
    internal class SaveableTerrainTexture : MonoBehaviour,
        IInitializableTerrainModificationRequester,
        ISaveable<IReadOnlySaveable<Texture2D>>
    {
        [Serializable]
        public enum EncodingType
        {
            Png,
            Jpg,
            Exr,
            Tga,
        }

        [JsonObject(MemberSerialization.OptOut)]
        [DataContract]
        [Serializable]
        public struct SaveableRawTexture : IReadOnlySaveable<Texture2D>
        {
            public byte[] rawTextureData;
            public int width;
            public int height;
            public GraphicsFormat graphicsFormat;

            public SaveableRawTexture(byte[] rawTextureData, int width, int height, GraphicsFormat graphicsFormat)
            {
                this.rawTextureData = rawTextureData;
                this.width = width;
                this.height = height;
                this.graphicsFormat = graphicsFormat;
            }

            public readonly Texture2D AsTexture()
            {
                Texture2D texture = new Texture2D(width, height, graphicsFormat, TextureCreationFlags.None);
                return AsTexture(texture);
            }

            public readonly Texture2D AsTexture(Texture2D texture)
            {
                texture.LoadRawTextureData(rawTextureData);
                texture.Apply();
                return texture;
            }

            public readonly Texture2D GetData() => AsTexture();
            public static SaveableRawTexture From(Texture2D texture)
            {
                return new SaveableRawTexture(texture.GetRawTextureData(), texture.width, texture.height, texture.graphicsFormat);
            }
        }

        [JsonObject(MemberSerialization.OptOut)]
        [DataContract]
        [Serializable]
        public struct SaveableEncodedTexture : IReadOnlySaveable<Texture2D>
        {
            public byte[] encodedTextureData;
            public GraphicsFormat graphicsFormat;

            public SaveableEncodedTexture(byte[] encodedTextureData, GraphicsFormat graphicsFormat)
            {
                this.encodedTextureData = encodedTextureData;
                this.graphicsFormat = graphicsFormat;
            }

            public readonly Texture2D AsTexture()
            {
                Texture2D texture = new Texture2D(2, 2, graphicsFormat, TextureCreationFlags.None);
                return AsTexture(texture);
            }

            public readonly Texture2D AsTexture(Texture2D texture)
            {
                ImageConversion.LoadImage(texture, encodedTextureData);
                return texture;
            }

            public readonly Texture2D GetData() => AsTexture();

            public static SaveableEncodedTexture From(Texture2D texture, EncodingType encodingType)
            {
                return new SaveableEncodedTexture(encodingType switch
                {
                    EncodingType.Png => texture.EncodeToPNG(),
                    EncodingType.Jpg => texture.EncodeToJPG(),
                    EncodingType.Exr => texture.EncodeToEXR(),
                    EncodingType.Tga => texture.EncodeToTGA(),
                    _ => texture.EncodeToPNG(),
                },
                texture.graphicsFormat);
            }
        }

        [SerializeField]
        private EncodingType _encodingType = EncodingType.Png;
        [SerializeField]
        private bool _serializeRawTexture = false;

        private IInitializableTerrainModificationRequester _terrainModificationRequester;
        private RenderTexture _terrainTexture;
        private RenderTexture _terrainWindowTexture;
        private Camera _camera;

        public bool Initialized => _terrainModificationRequester.Initialized;

        public IReadOnlySaveable<Texture2D> GetData() =>
            _serializeRawTexture
            ? SaveableRawTexture.From(FromRenderTexture(_terrainTexture))
            : SaveableEncodedTexture.From(FromRenderTexture(_terrainTexture), _encodingType);

        public bool TrySetData(IReadOnlySaveable<Texture2D> saveData)
        {
            Graphics.Blit(saveData.GetData(), _terrainTexture);
            return true;
        }

        private void Awake()
        {
            _terrainModificationRequester =
                GetComponentsInChildren<IInitializableTerrainModificationRequester>()
                .FirstOrDefault(i => i != (IInitializableTerrainModificationRequester)this);
        }

        public bool Initialize(RenderTexture terrainTexture, RenderTexture terrainWindowTexture, Camera camera)
        {
            (_terrainTexture, _terrainWindowTexture, _camera) = (terrainTexture, terrainWindowTexture, camera);
            return _terrainModificationRequester.Initialize(
                _terrainTexture,
                _terrainWindowTexture,
                _camera);
        }

        public bool Initialize(Vector2Int terrainTextureSize, Vector2Int terrainWindowTextureSize, Camera camera, out RenderTexture terrainRenderTexture, out RenderTexture terrainWindowRenderTexture)
        {
            bool success = _terrainModificationRequester.Initialize(
                terrainTextureSize,
                terrainWindowTextureSize,
                _camera = camera,
                out _terrainTexture,
                out _terrainWindowTexture);

            terrainRenderTexture = _terrainTexture;
            terrainWindowRenderTexture = _terrainWindowTexture;
            return success;
        }

        public bool Finalize() => _terrainModificationRequester.Finalize();

        public static Texture2D FromRenderTexture(RenderTexture renderTexture)
        {
            RenderTexture.active = renderTexture;

            Texture2D destination = new Texture2D(renderTexture.width, renderTexture.height, renderTexture.graphicsFormat, TextureCreationFlags.None);
            destination.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            RenderTexture.active = null;
            return destination;
        }

        public static Texture2D FromTexture(Texture texture)
        {
            RenderTexture temporaryBuffer = RenderTexture.GetTemporary(texture.width, texture.height, 0, texture.graphicsFormat);
            Graphics.Blit(texture, temporaryBuffer);

            Texture2D destination = FromRenderTexture(temporaryBuffer);
            RenderTexture.ReleaseTemporary(temporaryBuffer);

            return destination;
        }
    }
}