window.downloadFile = function (fileName, contentType, base64Content) {
    const linkElement = document.createElement('a');
    linkElement.href = `data:${contentType};base64,${base64Content}`;
    linkElement.download = fileName;
    linkElement.click();
};
