package com.chattalk.auth.controller;

import com.chattalk.auth.common.DataMap;
import com.chattalk.auth.common.util.JsonUtil;
import com.chattalk.auth.service.FileApiService;
import com.fasterxml.jackson.databind.ObjectMapper;
import jakarta.servlet.http.HttpServletRequest;
import lombok.extern.slf4j.Slf4j;
import org.springframework.core.io.Resource;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.multipart.MultipartHttpServletRequest;

import java.util.Arrays;
import java.util.Map;

@Slf4j
@RestController
@RequestMapping("/file")
public class AttachFileController {
    private final FileApiService fileApiService;

    public AttachFileController(FileApiService fileApiService) {
        this.fileApiService = fileApiService;
    }

    @PostMapping("/upload")
    public int upload(@RequestParam("file") MultipartFile file,
                      @RequestParam("requests") String requests) {
        DataMap dataMap = JsonUtil.toDataMap(requests);
        return fileApiService.upload(file, dataMap);
    }

    @GetMapping("/download")
    public ResponseEntity<Resource> download(@RequestParam String refType,
                                             @RequestParam String refId) {
        DataMap dataMap = new DataMap();
        dataMap.setParam("refType", refType);
        dataMap.setParam("refId", refId);
        return fileApiService.download(dataMap);
    }
}
