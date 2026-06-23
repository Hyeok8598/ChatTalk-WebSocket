package com.chattalk.auth.mapper;

import com.chattalk.auth.common.DataMap;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface AttachFileMapper {
    int insertAttachFile001(DataMap dataMap);
    DataMap selectOneAttachFile001(DataMap dataMap);
}