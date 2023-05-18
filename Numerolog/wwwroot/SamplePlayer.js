export function GetUrl(wavBytes) {
    var blob = new Blob([wavBytes], { type: "audio/x-wav" });
    var url = URL.createObjectURL(blob);
    return url;
}
//# sourceMappingURL=SamplePlayer.js.map