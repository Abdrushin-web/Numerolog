export function GetUrl(wavBytes, type) {
    var blob = new Blob([wavBytes], { type: type });
    var url = URL.createObjectURL(blob);
    return url;
}
//# sourceMappingURL=AudioPlayer.js.map