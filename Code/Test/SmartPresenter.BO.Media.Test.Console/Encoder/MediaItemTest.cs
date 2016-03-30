using SmartPresenter.BO.Media.Encoder;
using System;

namespace SmartPresenter.BO.Media.Test.Console.Encoder
{
    public static class MediaItemTest
    {
        public static void EncodeTest()
        {
            MediaItem mediaItem = new MediaItem(@"B:\TestVideo\5.wmv");
            //MediaItem mediaItem = new MediaItem(@"B:\TestVideo\10.mp3");
            mediaItem.OutputPath = @"B:\Video\";

            if (mediaItem.SourceAudioProfile == null) throw new Exception();
            if (mediaItem.SourceVideoProfile == null) throw new Exception();
            if (mediaItem.TargetAudioProfile == null) throw new Exception();
            if (mediaItem.TargetVideoProfile == null) throw new Exception();
            if (mediaItem.Markers == null) throw new Exception();
            if (string.IsNullOrEmpty(mediaItem.FileName)) throw new Exception();
            if (string.IsNullOrEmpty(mediaItem.OutputPath)) throw new Exception();

            mediaItem.Markers.Add(new Marker(TimeSpan.FromSeconds(5), "Start"));
            mediaItem.Markers.Add(new Marker(TimeSpan.FromSeconds(65), "End"));

            //mediaItem.Clip(TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(65));
            //mediaItem.ConvertVideoFormat(VideoFormat.MKV);
            //mediaItem.ConvertAudioFormat(AudioFormat.WMA);
            //mediaItem.Clip(TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(65));
            mediaItem.CropVideo(100, 100, 1080, 520);
        }
    }
}
