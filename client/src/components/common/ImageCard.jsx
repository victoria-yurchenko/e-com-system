import React from "react";
import { Box, Card, CardContent, Grid2, Typography, useMediaQuery, useTheme } from "@mui/material";

const ImageCard = ({ text, imgSrc, imgAlt, imgHeight, isImgFirst }) => {
  const theme = useTheme();
  const isSmallScreenSize = useMediaQuery(theme.breakpoints.down("md")); 

  return (
    <Box sx={{ my: 4 }}>
      <Card sx={{ display: "flex", alignItems: "center", p: 2 }}>
        <Grid2
          container
          spacing={2}
          alignItems="center"
          justifyContent="center"
          wrap={isSmallScreenSize ? "wrap" : "nowrap"} 
          direction={isSmallScreenSize ? "column" : "row"} 
        >
          {(isImgFirst || isSmallScreenSize) && (
            <Grid2 item sx={{ flexShrink: 0 }}>
              <img src={imgSrc} alt={imgAlt} height={imgHeight} style={{ maxWidth: "100%", display: "block" }} />
            </Grid2>
          )}

          <Grid2 item xs>
            <CardContent>
              <Typography variant="h5" align={isSmallScreenSize ? "center" : "left"}>
                {text}
              </Typography>
            </CardContent>
          </Grid2>

          {!isImgFirst && !isSmallScreenSize && (
            <Grid2 item sx={{ flexShrink: 0 }}>
              <img src={imgSrc} alt={imgAlt} height={imgHeight} style={{ maxWidth: "100%", display: "block" }} />
            </Grid2>
          )}
        </Grid2>
      </Card>
    </Box>
  );
};

export default ImageCard;
