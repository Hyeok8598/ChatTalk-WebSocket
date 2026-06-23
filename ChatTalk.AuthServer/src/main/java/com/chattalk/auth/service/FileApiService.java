package com.chattalk.auth.service;

import com.chattalk.auth.common.DataMap;
import com.chattalk.auth.common.constant.FileConst;
import com.chattalk.auth.common.util.CommonUtil;
import com.chattalk.auth.common.util.FileUtil;
import lombok.extern.slf4j.Slf4j;
import org.springframework.core.io.FileSystemResource;
import org.springframework.core.io.Resource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.UUID;

@Slf4j
@Service
public class FileApiService {
    private final AttachFileService service;

    public FileApiService(AttachFileService service) {
        this.service = service;
    }

    public int upload(MultipartFile file, DataMap dataMap) {
        if(file.isEmpty()) {
            throw new IllegalArgumentException("파일은 필수입니다.");
        }

        String fileId = UUID.randomUUID().toString();
        String originalFileName = file.getOriginalFilename();
        String fileDate = LocalDate.now().format(DateTimeFormatter.ofPattern("yyyyMMdd"));
        String storedFileName = FileUtil.toStoredName(originalFileName,String.format("%s_%s", fileDate, fileId));
        String filePath = FileConst.UPLOAD_PATH + FileConst.CHAT_UPLOAD_PATH;
        FileUtil.saveFile(file, storedFileName, FileConst.CHAT_UPLOAD_PATH);

        DataMap result = new DataMap();
        result.put("fileId", fileId);
        result.put("originalName", originalFileName);
        result.put("storedName", storedFileName);
        result.put("filePath", filePath);
        result.put("fileSize", file.getSize());
        result.put("contentType", file.getContentType());
        result.put("createdBy", dataMap.get("createBy"));
        result.put("refType", dataMap.get("refType"));
        result.put("refId", dataMap.get("refId"));
        return service.upload(result);
    }

    public ResponseEntity<Resource> download(DataMap dataMap) {
        DataMap file = service.download(dataMap);

        if(file == null) {
            throw new IllegalArgumentException("파일이 존재하지 않습니다.");
        }

        log.info("### LOG : {}", file);
        String filePath = file.getParam("filePath");
        log.info("### LOG : {}", filePath);
        String storedFileName = file.getParam("storedName");

        Path path = Paths.get(filePath).resolve(storedFileName);
        Resource resource = new FileSystemResource(path);

        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION,
                        "attachment; filename=\"" + storedFileName + "\"")
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(resource);
    }
}
