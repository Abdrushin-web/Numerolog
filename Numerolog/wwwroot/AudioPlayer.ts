export function GetUrl(wavBytes: Uint8Array, type: string) {
    const blob = new Blob([wavBytes], { type: type });
    const url = URL.createObjectURL(blob);
    return url;
}