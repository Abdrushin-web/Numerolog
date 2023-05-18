export function GetUrl(wavBytes: Uint8Array) {
    const blob = new Blob([wavBytes], { type: "audio/x-wav" });
    const url = URL.createObjectURL(blob);
    return url;
}