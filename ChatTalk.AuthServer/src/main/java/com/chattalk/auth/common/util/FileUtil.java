package com.chattalk.auth.common.util;

import com.chattalk.auth.common.constant.FileConst;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

@Slf4j
public final class FileUtil {
    private FileUtil() {};

    public static String toStoredName(String originName, String changeName) {
        String extension = getExtension(originName);
        return changeName + extension;
    }

    public static void saveFile(MultipartFile file, String fileName, String directory) {
        if(CommonUtil.isEmpty(file)) return;

        try {
            Path uploadPath = Paths.get(FileConst.UPLOAD_PATH, directory);
            Path savePath   = uploadPath.resolve(fileName);
            if(!mkdirs(uploadPath)) return;
            file.transferTo(savePath.toFile());
        } catch (IOException e) {
            throw new RuntimeException("파일 저장 중 오류가 발생했습니다.", e);
        }
    }

    private static boolean mkdirs(Path path) {
        try {
            Files.createDirectories(path);
            return true;
        } catch (IOException e) {
            return false;
        }
    }

    private static String getExtension(String fileName) {
        if(CommonUtil.isEmpty(fileName) || !fileName.contains(".")) return "";
        return fileName.substring(fileName.lastIndexOf("."));
    }
}