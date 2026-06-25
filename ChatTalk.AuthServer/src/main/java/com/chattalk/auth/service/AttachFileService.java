package com.chattalk.auth.service;

import com.chattalk.auth.common.DataMap;
import com.chattalk.auth.common.util.CommonUtil;
import com.chattalk.auth.mapper.AttachFileMapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Slf4j
@Service
public class AttachFileService {
    private AttachFileMapper mapper;

    public AttachFileService(AttachFileMapper mapper) {
        this.mapper = mapper;
    }

    public int upload(DataMap dataMap) {
        String fileId = dataMap.getParam("fileId");
        String originalName = dataMap.getParam("originalName");
        String storedName = dataMap.getParam("storedName");
        String filePath = dataMap.getParam("filePath");
        long fileSize = dataMap.getParam("fileSize");
        String refType = dataMap.getParam("refType");
        String refId = dataMap.getParam("refId");

        if(CommonUtil.isEmpty(fileId)) {
            throw new IllegalArgumentException("파일 ID는 필수입니다.");
        }

        if(CommonUtil.isEmpty(originalName)) {
            throw new IllegalArgumentException("원본 파일명은 필수입니다.");
        }

        if(CommonUtil.isEmpty(storedName)) {
            throw new IllegalArgumentException("저장 파일명은 필수입니다.");
        }

        if(CommonUtil.isEmpty(filePath)) {
            throw new IllegalArgumentException("파일 경로는 필수입니다.");
        }

        if(CommonUtil.isEmpty(fileSize)) {
            throw new IllegalArgumentException("파일 Size는 필수입니다.");
        }

        if(CommonUtil.isEmpty(refType)) {
            throw new IllegalArgumentException("Ref Type은 필수입니다.");
        }

        if(CommonUtil.isEmpty(refId)) {
            throw new IllegalArgumentException("Ref ID는 필수입니다.");
        }

        return mapper.insertAttachFile001(dataMap);
    }

    public DataMap download(DataMap dataMap) {
        String refType = dataMap.getParam("refType");
        String refId = dataMap.getParam("refId");

        if(CommonUtil.isEmpty(refType)) {
            throw new IllegalArgumentException("Ref Type은 필수입니다.");
        }

        if(CommonUtil.isEmpty(refId)) {
            throw new IllegalArgumentException("Ref ID는 필수입니다.");
        }

        return mapper.selectOneAttachFile001(dataMap);
    }
}
