export async function downloadAttachFile(request, originalName) {
    const refType = request.refType;
    const refId = request.refId;


    const response = await downloadFile(request);

    const blob = new Blob([response.data], {
        type: response.headers['content-type']
    });

    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = originalName || 'download';
    document.body.appendChild(a);
    a.click();
    a.remove();
    window.URL.revokeObjectURL(url);
};