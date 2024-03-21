﻿using System.IO;
using UnityEngine;

namespace SaveSystem.SaveRequester.SavePath
{
    [CreateAssetMenu(fileName = "SavePathService", menuName = "SaveSystem/SavePathService")]
    internal class SavePathService : SavePathFlyweight, ISavePathService
    {
        [SerializeField]
        [TextArea]
        private string _relativePath;

        [SerializeField]
        private string _fileName;
        [SerializeField]
        private string _fileExtension;

        public override string GetPath() =>
            Path.ChangeExtension(
                Path.Combine(Application.persistentDataPath, _relativePath, _fileName),
                _fileExtension);
    }
}