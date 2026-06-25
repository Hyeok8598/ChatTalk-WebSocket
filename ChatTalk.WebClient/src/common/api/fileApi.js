import axios from 'axios';

export async function uploadFile(file, request) {
    if(!file) {
        throw new Error("File is null.");
    }

    const formData = new FormData();
    formData.append('file', file);
    formData.append('requests', JSON.stringify(request));

    const response = await axios.post(
        'http://localhost:8080/file/' + "upload",
        formData,
        {
            headers: {
                'Content-Type' : 'multipart/form-data'
            }
        }
    );
    return response.data;
};

export async function downloadFile(request) {
    const response = await axios.get(
        `/file/download?refType=${refType}&refId=${refId}`, 
        {
            responseType: 'blob'
        }
    );

    return response;
};